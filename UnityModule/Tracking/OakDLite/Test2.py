import depthai as dai
import cv2
import numpy as np

# Define constants for ball detection
MIN_RADIUS = 15  # Minimum radius of detected circles
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
    color_cam = pipeline.createColorCamera()
    xout_video = pipeline.createXLinkOut()
    xout_video.setStreamName("video")

    # Set camera properties
    color_cam.setBoardSocket(dai.CameraBoardSocket.RGB)
    color_cam.setResolution(dai.ColorCameraProperties.SensorResolution.THE_1080_P)
    color_cam.setVideoSize(1920, 1080)
    color_cam.setBoardSocket(dai.CameraBoardSocket.RGB)

    # Connect the color camera to the video output
    color_cam.video.link(xout_video.input)

    # Start pipeline
    device = dai.Device(pipeline)
    device.startPipeline()

    # Output queues
    video_queue = device.getOutputQueue(name="video", maxSize=4, blocking=False)

    while True:
        # Get color frame
        video_data = video_queue.get()
        color_frame = video_data.getCvFrame()

        # Convert color frame to grayscale
        gray_frame = cv2.cvtColor(color_frame, cv2.COLOR_BGR2GRAY)

        # Perform Hough Circle Transform
        circles = cv2.HoughCircles(gray_frame, cv2.HOUGH_GRADIENT, dp=1, minDist=10,
                                   param1=55, param2=30, minRadius=MIN_RADIUS, maxRadius=MAX_RADIUS)

        if circles is not None:
            # Detected circles
            circles = np.round(circles[0, :]).astype(int)

            for (x, y, r) in circles:
                # Calculate ball position and distance
                ball_x = int(x * POOL_TABLE_WIDTH / color_frame.shape[1])
                ball_y = int(y * POOL_TABLE_HEIGHT / color_frame.shape[0])
                ball_radius = int(r * POOL_TABLE_WIDTH / color_frame.shape[1])
                ball_distance = CALIBRATED_DISTANCE * ball_radius / r

                # Print ball position and distance
                print(f"Ball Position: ({ball_x}, {ball_y}), Distance: {ball_distance}mm")

                # Draw circle on the color frame
                cv2.circle(color_frame, (x, y), r, (0, 255, 0), 2)

        # Display color frame
        cv2.imshow("Color", color_frame)

        if cv2.waitKey(1) == ord('q'):
            break

    # Release resources
    cv2.destroyAllWindows()

if __name__ == '__main__':
    main()
