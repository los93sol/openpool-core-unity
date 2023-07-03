import cv2
import numpy as np

# Global variables
calibration_completed = [False]
ball_properties = {'ball_radius': 0}

def calibrate_ball(frame):
    # Create a window for ball selection
    cv2.namedWindow("Ball Selection")
    cv2.imshow("Ball Selection", frame)

    roi_selected = False
    roi_coordinates = []

    def select_ball(event, x, y, flags, param):
        nonlocal roi_selected, roi_coordinates

        if event == cv2.EVENT_LBUTTONDOWN:
            if not roi_selected:
                # Start selecting the ROI
                roi_coordinates = [(x, y)]
                roi_selected = True

        elif event == cv2.EVENT_LBUTTONUP:
            if roi_selected:
                # Finish selecting the ROI
                roi_coordinates.append((x, y))
                if len(roi_coordinates) == 2:  # Check if both points have been selected
                    # Calculate the bounding box of the ROI
                    x_min = min(roi_coordinates[0][0], roi_coordinates[1][0])
                    x_max = max(roi_coordinates[0][0], roi_coordinates[1][0])
                    y_min = min(roi_coordinates[0][1], roi_coordinates[1][1])
                    y_max = max(roi_coordinates[0][1], roi_coordinates[1][1])

                    # Extract the region of interest (ROI)
                    roi = frame[y_min:y_max, x_min:x_max]

                    # Calculate the radius of the ball
                    ball_radius = roi.shape[0] // 2

                    # Update the ball properties
                    ball_properties['ball_radius'] = ball_radius

                    # Mark calibration as completed
                    calibration_completed[0] = True

    # Set mouse callback for ball selection
    cv2.setMouseCallback("Ball Selection", select_ball)

    while not calibration_completed[0]:
        # Show the selected ROI in the ball selection window
        if roi_selected:
            frame_with_roi = frame.copy()
            if len(roi_coordinates) == 2:  # Check if both points have been selected
                cv2.rectangle(frame_with_roi, roi_coordinates[0], roi_coordinates[1], (0, 255, 0), 2)
            cv2.imshow("Ball Selection", frame_with_roi)

        # Break the loop if the user closes the window
        if cv2.waitKey(1) & 0xFF == 27:  # 27 is the ASCII value of the 'Esc' key
            break

    # Close the ball selection window
    cv2.destroyWindow("Ball Selection")

def detect_balls(frame):
    # Convert frame to grayscale
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

    # Apply Gaussian blur to reduce noise
    blurred = cv2.GaussianBlur(gray, (5, 5), 0)

    # Retrieve ball properties
    ball_radius = ball_properties['ball_radius']

    # Define the range of radii to search for based on the ball size
    min_radius = ball_radius - 20
    max_radius = ball_radius + 20

    # Apply Hough Circle Transform to detect the balls
    circles = cv2.HoughCircles(blurred, cv2.HOUGH_GRADIENT, dp=1, minDist=40,
                               param1=50, param2=30, minRadius=min_radius, maxRadius=max_radius)

    # Draw circles on the frame if any are detected
    if circles is not None:
        # Convert the circle parameters to integers
        circles = np.round(circles[0, :]).astype("int")

        # Iterate over detected circles
        for (x, y, r) in circles:
            # Draw the ball on the frame
            cv2.circle(frame, (x, y), r, (0, 255, 0), 2)

    return frame

# Open web camera
cap = cv2.VideoCapture(0)

# Main loop
while True:
    # Read frame from web camera
    ret, frame = cap.read()

    # Flip the frame horizontally and vertically for mirrored display
    frame = cv2.flip(frame, -1)

    if not calibration_completed[0]:
        # Calibrate the ball
        calibrate_ball(frame)
    else:
        # Perform ball detection
        frame_with_detection = detect_balls(frame)

        # Show the frame with ball detection
        cv2.imshow("Pool Balls Detection", frame_with_detection)

    # Break the loop on 'q' key press
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# Release the web camera and close the windows
cap.release()
cv2.destroyAllWindows()
