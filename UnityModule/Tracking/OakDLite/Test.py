import depthai
import numpy as np
import cv2

# Define the depth threshold for ball detection (adjust according to your setup)
depth_threshold = 0.2  # Example value, units in meters

# Initialize the Oak-D camera
pipeline = depthai.Pipeline()
pipeline.setOpenVINOVersion(version=depthai.OpenVINO.Version.VERSION_2021_4)

# Define the left and right camera streams
left_cam = pipeline.createMonoCamera()
left_cam.setBoardSocket(depthai.CameraBoardSocket.LEFT)

right_cam = pipeline.createMonoCamera()
right_cam.setBoardSocket(depthai.CameraBoardSocket.RIGHT)

# Define the stereo depth stream
stereo_depth = pipeline.createStereoDepth()
stereo_depth.setConfidenceThreshold(255)
stereo_depth.setRectifyEdgeFillColor(0)

# Create an output queue for the stereo depth stream
depth_queue = pipeline.createXLinkOut()
depth_queue.setStreamName("depth")

# Connect the left and right camera streams to the stereo depth node
left_cam.out.link(stereo_depth.left)
right_cam.out.link(stereo_depth.right)

# Connect the stereo depth stream output to the depth queue
stereo_depth.disparity.link(depth_queue.input)

# Create and start the device
device = depthai.Device(pipeline)
device.startPipeline()

# Main loop
while True:
    # Retrieve the depth frames
    depth_packet = device.getOutputQueue("depth").get()

    # Get the depth map and normalize it
    depth_map = depth_packet.getFrame()

    # Normalize the depth map for visualization
    depth_map_normalized = (depth_map - depth_map.min()) / (depth_map.max() - depth_map.min())
    depth_map_normalized = (depth_map_normalized * 255).astype(np.uint8)

    # Apply a colormap to the depth map for better visualization
    depth_map_viz = cv2.applyColorMap(depth_map_normalized, cv2.COLORMAP_JET)

    # Perform ball detection using depth threshold
    ball_mask = np.where(depth_map < depth_threshold, 255, 0).astype(np.uint8)

    # Find contours of detected balls
    contours, _ = cv2.findContours(ball_mask, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    # Define a list to store detected ball contours
    detected_balls = []

    # Process each contour
    for contour in contours:
        # Calculate the contour area
        area = cv2.contourArea(contour)

        # Check if the contour area corresponds to a ball
        if area > 100:  # Example threshold value for minimum contour area
            # Calculate the bounding box coordinates
            x, y, w, h = cv2.boundingRect(contour)

            # Draw bounding box on the depth map visualization
            cv2.rectangle(depth_map_viz, (x, y), (x + w, y + h), (0, 255, 0), 2)

            # Add the contour to the list of detected balls
            detected_balls.append(contour)

    # Print the number of detected balls
    print(f"{len(detected_balls)} balls detected!")

    # Display the depth map
    cv2.imshow("Depth Map", depth_map_viz)

    # Exit the loop if 'q' is pressed
    if cv2.waitKey(1) == ord("q"):
        break

# Cleanup
cv2.destroyAllWindows()
device.close()
