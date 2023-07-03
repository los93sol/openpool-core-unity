import cv2
import depthai

# DepthAI pipeline configuration
pipeline = depthai.Pipeline()

# Configure the stereo cameras
left_cam = pipeline.createMonoCamera()
left_cam.setResolution(depthai.MonoCameraProperties.SensorResolution.THE_400_P)
left_cam.setBoardSocket(depthai.CameraBoardSocket.LEFT)

right_cam = pipeline.createMonoCamera()
right_cam.setResolution(depthai.MonoCameraProperties.SensorResolution.THE_400_P)
right_cam.setBoardSocket(depthai.CameraBoardSocket.RIGHT)

# Configure the depth calculation
stereo = pipeline.createStereoDepth()
stereo.setOutputDepth(True)

# Connect left and right cameras to stereo depth
left_cam.out.link(stereo.left)
right_cam.out.link(stereo.right)

# Create an output stream for depth frames
xout_depth = pipeline.createXLinkOut()
xout_depth.setStreamName("depth")
stereo.depth.link(xout_depth.input)

# Create an output stream for color frames
xout_color = pipeline.createXLinkOut()
xout_color.setStreamName("color")
left_cam.out.link(xout_color.input)

# Create the DepthAI device and start the pipeline
device = depthai.Device(pipeline)
device.startPipeline()

# Create an output queue for depth frames
q_depth = device.getOutputQueue("depth", maxSize=1, blocking=False)

# Create an output queue for color frames
q_color = device.getOutputQueue("color", maxSize=1, blocking=False)

# Create an OpenCV window
window_name = "Depth and Color"
cv2.namedWindow(window_name)

# Variables to store the most recent click coordinates
recent_click_x = None
recent_click_y = None

# Mouse event callback function
def mouse_callback(event, x, y, flags, param):
    global recent_click_x, recent_click_y
    if event == cv2.EVENT_LBUTTONDOWN:
        recent_click_x = x
        recent_click_y = y

# Set the mouse callback
cv2.setMouseCallback(window_name, mouse_callback)

# Main loop
while True:
    # Check if there was a recent click
    if recent_click_x is not None and recent_click_y is not None:
        # Retrieve the depth frame and color frame
        depth_frame = q_depth.tryGet()
        color_frame = q_color.tryGet()

        if depth_frame is not None and color_frame is not None:
            # Retrieve the depth frame as a numpy array
            depth_data = depth_frame.getFrame()

            # Retrieve the depth value at the clicked point
            depth_value = depth_data[recent_click_y, recent_click_x]

            # Retrieve the color frame as a numpy array
            color_data = color_frame.getCvFrame()

            # Display the depth and color information
            print("Depth at ({}, {}): {}".format(recent_click_x, recent_click_y, depth_value))
            print("Color at ({}, {}): {}".format(recent_click_x, recent_click_y, color_data[recent_click_y, recent_click_x]))

            # Reset the recent click coordinates
            recent_click_x = None
            recent_click_y = None

    # Retrieve the depth frame and color frame
    depth_frame = q_depth.tryGet()
    color_frame = q_color.tryGet()

    if depth_frame is not None and color_frame is not None:
        # Retrieve the depth frame as a numpy array
        depth_data = depth_frame.getFrame()

        # Retrieve the color frame as a numpy array
        color_data = color_frame.getCvFrame()

        # Convert the depth frame to a visual representation
        depth_frame_visual = (depth_data * (255 / stereo.getMaxDisparity())).astype('uint8')

        # Display the depth frame and color frame
        cv2.imshow(window_name, cv2.hconcat([depth_frame_visual, color_data]))

    # Check for the 'q' key press to exit the loop
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# Release resources
cv2.destroyAllWindows()
device.close()
