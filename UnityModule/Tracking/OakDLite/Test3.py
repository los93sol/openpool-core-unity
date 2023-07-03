import depthai as dai
import cv2
import numpy as np

# Define constants for ball detection
MIN_RADIUS = 10  # Minimum radius of detected circles
MAX_RADIUS = 30  # Maximum radius of detected circles

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
    manip_left = pipeline.createImageManip()
    manip_right = pipeline.createImageManip()
    xout_left = pipeline.createXLinkOut()
    xout_left.setStreamName("left")
    xout_right = pipeline.createXLinkOut()
    xout_right.setStreamName("right")

    # Set camera properties
    left_cam.setResolution(dai.MonoCameraProperties.SensorResolution.THE_720_P)
    right_cam.setResolution(dai.MonoCameraProperties.SensorResolution.THE_720_P)

    # Configure image manipulation for left camera
    manip_left.initialConfig.setResize(300, 300)
    manip_left.initialConfig.setFrameType(dai.ImgFrame.Type.BGR888p)

    # Configure image manipulation for right camera
    manip_right.initialConfig.setResize(300, 300)
    manip_right.initialConfig.setFrameType(dai.ImgFrame.Type.BGR888p)

    # Connect nodes
    left_cam.out.link(manip_left.inputImage)
    manip_left.out.link(xout_left.input)
    right_cam.out.link(manip_right.inputImage)
    manip_right.out.link(xout_right.input)

    # Start pipeline
    device = dai.Device(pipeline)
    device.startPipeline()

    # Output queues
    left_queue = device.getOutputQueue(name="left", maxSize=4, blocking=False)
    right_queue = device.getOutputQueue(name="right", maxSize=4, blocking=False)

    while True:
        # Get left frame
        left_data = left_queue.get()
        left_frame = left_data.getCvFrame()

        # Convert left frame to grayscale
        gray_left_frame = cv2.cvtColor(left_frame, cv2.COLOR_BGR2GRAY)

        # Perform Hough Circle Transform on left frame
        left_circles = cv2.HoughCircles(gray_left_frame, cv2.HOUGH_GRADIENT, dp=1, minDist=100,
                                        param1=50, param2=30, minRadius=MIN_RADIUS, maxRadius=MAX_RADIUS)

        # Get right frame
        right_data = right_queue.get()
        right_frame = right_data.getCvFrame()

        # Convert right frame to grayscale
        gray_right_frame = cv2.cvtColor(right_frame, cv2.COLOR_BGR2GRAY)

        # Perform Hough Circle Transform on right frame
        right_circles = cv2.HoughCircles(gray_right_frame, cv2.HOUGH_GRADIENT, dp=1, minDist=100,
                                         param1=50, param2=30, minRadius=MIN_RADIUS, maxRadius=MAX_RADIUS)

        if left_circles is not None:
            # Detected circles in left frame
            left_circles = np.round(left_circles[0, :]).astype(int)

            for (x, y, r) in left_circles:
                # Calculate ball position and distance
                ball_x = int(x * POOL_TABLE_WIDTH / left_frame.shape[1])
                ball_y = int(y * POOL_TABLE_HEIGHT / left_frame.shape[0])
                ball_radius = int(r * POOL_TABLE_WIDTH / left_frame.shape[1])
                ball_distance = CALIBRATED_DISTANCE * ball_radius / r

                # Print ball position and distance
                print(f"Left Ball Position: ({ball_x}, {ball_y}), Distance: {ball_distance}mm")

                # Draw circle on the left frame
                cv2.circle(left_frame, (x, y), r, (0, 255, 0), 2)

        if right_circles is not None:
            # Detected circles in right frame
            right_circles = np.round(right_circles[0, :]).astype(int)

            for (x, y, r) in right_circles:
                # Calculate ball position and distance
                ball_x = int(x * POOL_TABLE_WIDTH / right_frame.shape[1])
                ball_y = int(y * POOL_TABLE_HEIGHT / right_frame.shape[0])
                ball_radius = int(r * POOL_TABLE_WIDTH / right_frame.shape[1])
                ball_distance = CALIBRATED_DISTANCE * ball_radius / r

                # Print ball position and distance
                print(f"Right Ball Position: ({ball_x}, {ball_y}), Distance: {ball_distance}mm")

                # Draw circle on the right frame
                cv2.circle(right_frame, (x, y), r, (0, 255, 0), 2)

        # Display frames
        cv2.imshow("Left Camera", left_frame)
        cv2.imshow("Right Camera", right_frame)

        if cv2.waitKey(1) == ord('q'):
            break

    # Release resources
    cv2.destroyAllWindows()

if __name__ == '__main__':
    main()
