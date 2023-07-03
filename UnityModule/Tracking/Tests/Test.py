import cv2
import numpy as np

def detect_balls(frame, ball_properties):
    # Convert frame to grayscale
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

    # Apply Gaussian blur to reduce noise
    blurred = cv2.GaussianBlur(gray, (5, 5), 0)

    # Define the range of radii to search for based on the ball size
    min_radius = ball_properties['ball_radius'] - 10
    max_radius = ball_properties['ball_radius'] + 10

    # Apply Hough Circle Transform to detect the balls
    circles = cv2.HoughCircles(blurred, cv2.HOUGH_GRADIENT, dp=1, minDist=50,
                               param1=50, param2=30, minRadius=min_radius, maxRadius=max_radius)

    # Check if any balls are detected
    if circles is not None:
        # Convert the circle parameters to integers
        circles = np.round(circles[0, :]).astype("int")

        # Iterate over detected circles
        for (x, y, r) in circles:
            # Draw the ball on the frame
            cv2.circle(frame, (x, y), r, (0, 255, 0), 2)

    return frame

def select_roi(event, x, y, flags, param):
    global roi_pts, selecting_roi

    if event == cv2.EVENT_LBUTTONDOWN:
        roi_pts = [(x, y)]
        selecting_roi = True

    elif event == cv2.EVENT_LBUTTONUP:
        roi_pts.append((x, y))
        selecting_roi = False
        cv2.rectangle(frame, roi_pts[0], roi_pts[1], (0, 255, 0), 2)
        cv2.imshow("Capture", frame)

# Initialize variables
roi_pts = []
selecting_roi = False
ball_properties = {'ball_radius': 0}  # Initialize ball properties

# Create window and set mouse callback for region of interest selection
cv2.namedWindow("Capture")
cv2.setMouseCallback("Capture", select_roi)

# Open webcam
cap = cv2.VideoCapture(0)

# Capture and process frames
while True:
    # Read frame from webcam
    ret, frame = cap.read()

    # Display frame
    cv2.imshow("Capture", frame)

    # Check if region of interest is selected
    if len(roi_pts) == 2:
        # Crop region of interest
        roi = frame[roi_pts[0][1]:roi_pts[1][1], roi_pts[0][0]:roi_pts[1][0]]

        # Convert region of interest to grayscale
        gray_roi = cv2.cvtColor(roi, cv2.COLOR_BGR2GRAY)

        # Apply Gaussian blur to reduce noise
        blurred_roi = cv2.GaussianBlur(gray_roi, (5, 5), 0)

        # Apply Hough Circle Transform to detect the ball
        circles = cv2.HoughCircles(blurred_roi, cv2.HOUGH_GRADIENT, dp=1, minDist=50,
                                   param1=50, param2=30, minRadius=10, maxRadius=100)

        # Check if a ball is detected
        if circles is not None:
            # Extract ball information
            circle = np.round(circles[0, :]).astype("int")
            (ball_x, ball_y, ball_r) = circle[0]

            # Draw the detected ball on the original frame
            cv2.circle(frame, (ball_x + roi_pts[0][0], ball_y + roi_pts[0][1]), ball_r, (0, 255, 0), 2)

            # Update ball properties with captured information
            ball_properties['ball_radius'] = ball_r

        # Show the original frame with ball detection
        frame_with_detection = detect_balls(frame, ball_properties)
        cv2.imshow("Pool Balls Detection", frame_with_detection)

    # Break the loop on 'q' key press
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# Release the webcam and close windows
cap.release()
cv2.destroyAllWindows()
