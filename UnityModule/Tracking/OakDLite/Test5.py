import depthai as dai
import cv2
import numpy as np

# Define constants for ball detection
MIN_RADIUS = 0  # Minimum radius of detected circles
MAX_RADIUS = 15  # Maximum radius of detected circles

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
    left_cam.setResolution(dai.MonoCameraProperties.SensorResolution.THE_480_P)
    right_cam.setResolution(dai.MonoCameraProperties.SensorResolution.THE_480_P)

    # Configure image manipulation for left camera
    manip_left.initialConfig.setResize(320, 240)  # Adjust the resize dimensions as needed

    # Configure image manipulation for right camera
    manip_right.initialConfig.setResize(320, 240)  # Adjust the resize dimensions as needed

    # Connect nodes
    left_cam.out.link(manip_left.inputImage)
    right_cam.out.link(manip_right.inputImage)
    manip_left.out.link(xout_left.input)
    manip_right.out.link(xout_right.input)

    # Start pipeline
    with dai.Device(pipeline) as device:
        # Start the pipeline manually
        device.startPipeline()

        # Output queues
        left_queue = device.getOutputQueue(name="left", maxSize=4, blocking=False)
        right_queue = device.getOutputQueue(name="right", maxSize=4, blocking=False)

        while True:
            # Get left frame
            left_data = left_queue.get()
            left_frame = left_data.getFrame()

            # Get right frame
            right_data = right_queue.get()
            right_frame = right_data.getFrame()

            # Apply denoising to the frames
            left_denoised = cv2.fastNlMeansDenoising(left_frame, None, 10, 7, 21)
            right_denoised = cv2.fastNlMeansDenoising(right_frame, None, 10, 7, 21)

            # Perform Hough Circle Transform on denoised frames
            left_circles = cv2.HoughCircles(left_denoised, cv2.HOUGH_GRADIENT, dp=1, minDist=50,
                                            param1=50, param2=30, minRadius=MIN_RADIUS, maxRadius=MAX_RADIUS)

            right_circles = cv2.HoughCircles(right_denoised, cv2.HOUGH_GRADIENT, dp=1, minDist=50,
                                             param1=50, param2=30, minRadius=MIN_RADIUS, maxRadius=MAX_RADIUS)

            # Draw circles on the frames
            if left_circles is not None:
                left_circles = np.round(left_circles[0, :]).astype("int")
                for (x, y, r) in left_circles:
                    cv2.circle(left_frame, (x, y), r, (0, 255, 0), 4)
                    ball_distance = calculate_distance(r)
                    ball_position = calculate_position(x, y)
                    print("Left Ball - Distance: {} mm, Position: ({}, {})".format(ball_distance, ball_position[0],
                                                                                   ball_position[1]))

            if right_circles is not None:
                right_circles = np.round(right_circles[0, :]).astype("int")
                for (x, y, r) in right_circles:
                    cv2.circle(right_frame, (x, y), r, (0, 255, 0), 4)
                    ball_distance = calculate_distance(r)
                    ball_position = calculate_position(x, y)
                    print("Right Ball - Distance: {} mm, Position: ({}, {})".format(ball_distance, ball_position[0],
                                                                                    ball_position[1]))

            # Display the frames
            cv2.imshow("Left Camera", left_frame)
            cv2.imshow("Right Camera", right_frame)

            # Exit loop if 'q' is pressed
            if cv2.waitKey(1) == ord("q"):
                break

    # Release resources
    cv2.destroyAllWindows()


# Calculate ball distance based on calibrated distance and radius
def calculate_distance(radius):
    return CALIBRATED_DISTANCE * (MIN_RADIUS / radius)


# Calculate ball position based on image coordinates
def calculate_position(x, y):
    # Adjust the calculations based on the actual size of the pool table
    pos_x = (x / 320) * POOL_TABLE_WIDTH
    pos_y = (y / 240) * POOL_TABLE_HEIGHT
    return pos_x, pos_y


if __name__ == "__main__":
    main()
