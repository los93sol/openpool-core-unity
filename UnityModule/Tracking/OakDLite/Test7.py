import depthai as dai
import cv2
import numpy as np

# Define constants for ball detection
MIN_RADIUS = 10  # Minimum radius of detected spheres
MAX_RADIUS = 30  # Maximum radius of detected spheres

# Define the pool table size
POOL_TABLE_WIDTH = 2000  # Width of the pool table in mm
POOL_TABLE_HEIGHT = 4000  # Height of the pool table in mm

# Define the calibrated distance to the balls
CALIBRATED_DISTANCE = 1000  # Calibrated distance to the balls in mm

# Main script
def main():
    # Create pipeline
    pipeline = dai.Pipeline()

    # Create nodes
    left_cam = pipeline.createMonoCamera()
    right_cam = pipeline.createMonoCamera()
    xout_left = pipeline.createXLinkOut()
    xout_right = pipeline.createXLinkOut()
    xout_left.setStreamName("left")
    xout_right.setStreamName("right")

    # Set camera properties
    left_cam.setBoardSocket(dai.CameraBoardSocket.LEFT)
    right_cam.setBoardSocket(dai.CameraBoardSocket.RIGHT)

    # Create depth node
    depth = pipeline.createStereoDepth()
    depth.setConfidenceThreshold(200)

    # Create spatial location calculator node
    spatial_location_calculator = pipeline.createSpatialLocationCalculator()
    spatial_location_calculator.setWaitForConfigInput(True)
    spatial_location_calculator.inputDepth.setBlocking(False)
    spatial_location_calculator.inputDepth.setQueueSize(1)

    # Link nodes
    left_cam.out.link(depth.left)
    right_cam.out.link(depth.right)
    depth.depth.link(spatial_location_calculator.inputDepth)
    spatial_location_calculator.out.link(xout_left.input)
    spatial_location_calculator.passthroughDepth.link(xout_right.input)

    # Start pipeline
    device = dai.Device(pipeline)
    device.startPipeline()

    # Output queues
    left_queue = device.getOutputQueue(name="left", maxSize=4, blocking=False)
    right_queue = device.getOutputQueue(name="right", maxSize=4, blocking=False)

    while True:
        # Get left mono frame
        left_data = left_queue.get()
        left_frame = left_data.getFrame()

        # Get right mono frame
        right_data = right_queue.get()
        right_frame = right_data.getFrame()

        # Convert mono frames to grayscale
        left_gray = cv2.cvtColor(left_frame, cv2.COLOR_BGR2GRAY)
        right_gray = cv2.cvtColor(right_frame, cv2.COLOR_BGR2GRAY)

        # Perform disparity calculation
        disparity = np.abs(left_gray - right_gray)

        # Perform sphere detection
        spheres = cv2.HoughCircles(disparity, cv2.HOUGH_GRADIENT, dp=1, minDist=100,
                                   param1=50, param2=30, minRadius=MIN_RADIUS, maxRadius=MAX_RADIUS)

        if spheres is not None:
            # Detected spheres
            spheres = np.round(spheres[0, :]).astype(int)

            for (x, y, r) in spheres:
                # Calculate sphere position and distance
                sphere_x = int(x * POOL_TABLE_WIDTH / disparity.shape[1])
                sphere_y = int(y * POOL_TABLE_HEIGHT / disparity.shape[0])
                sphere_radius = int(r * POOL_TABLE_WIDTH / disparity.shape[1])
                sphere_distance = CALIBRATED_DISTANCE * sphere_radius / r

                # Print sphere position and distance
                print(f"Sphere Position: ({sphere_x}, {sphere_y}), Distance: {sphere_distance}mm")

                # Draw circle on the disparity frame
                cv2.circle(disparity, (x, y), r, (0, 255, 0), 2)

        # Display disparity frame
        cv2.imshow("Disparity", disparity)

        if cv2.waitKey(1) == ord('q'):
            break

    # Release resources
    cv2.destroyAllWindows()

if __name__ == '__main__':
    main()
