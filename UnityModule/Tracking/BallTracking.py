import cv2
import numpy as np
import os
from imutils.object_detection import non_max_suppression

ROI_FILE = "roi_points.npy"
TEMPLATES_DIR = "templates"

BALLS = ["Cue", "1", "2", "3", "4", "5"]  # Add more ball names if needed

def select_roi(image):
    roi_points = []

    def select_roi_callback(event, x, y, flags, param):
        nonlocal roi_points

        if event == cv2.EVENT_LBUTTONDOWN:
            roi_points.append((x, y))

        if len(roi_points) >= 4:
            # Draw the polygon on the image
            cv2.polylines(image, [np.array(roi_points)], True, (0, 255, 0), 2)

    cv2.namedWindow("Select ROI")
    cv2.setMouseCallback("Select ROI", select_roi_callback)

    while True:
        cv2.imshow("Select ROI", image)
        key = cv2.waitKey(1) & 0xFF

        if key == ord("r"):
            # Reset the ROI selection
            roi_points = []
            image_copy = image.copy()
            cv2.imshow("Select ROI", image_copy)

        elif key == ord("q"):
            # Quit the ROI selection
            break

    cv2.destroyAllWindows()

    return np.array(roi_points, dtype=np.float32)

def transform_image(image, roi_points):
    # Determine the maximum width and height of the transformed image
    max_width = max(np.linalg.norm(roi_points[0] - roi_points[1]), np.linalg.norm(roi_points[2] - roi_points[3]))
    max_height = max(np.linalg.norm(roi_points[0] - roi_points[3]), np.linalg.norm(roi_points[1] - roi_points[2]))

    # Create the destination points for the transformed image
    dst_points = np.array([[0, 0], [max_width - 1, 0], [max_width - 1, max_height - 1], [0, max_height - 1]],
                          dtype=np.float32)

    # Calculate the perspective transformation matrix
    M = cv2.getPerspectiveTransform(roi_points, dst_points)

    # Apply the perspective transformation to the image
    transformed_image = cv2.warpPerspective(image, M, (int(max_width), int(max_height)))

    return transformed_image

def track_balls(image):
    # Convert the image to grayscale
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # Initialize the list of detections
    detections = []

    # Iterate over the ball templates
    for ball_name in BALLS:
        # Load the template for the current ball
        template_file = os.path.join(TEMPLATES_DIR, f"{ball_name}_template.jpg")
        template = cv2.imread(template_file, cv2.IMREAD_GRAYSCALE)

        # Perform template matching
        result = cv2.matchTemplate(gray, template, cv2.TM_CCOEFF_NORMED)

        # Define a threshold for considering a match
        threshold = 0.8

        # Get the locations where the template matches the image above the threshold
        locations = np.where(result >= threshold)
        locations = list(zip(*locations[::-1]))  # Reverse (x, y) coordinates

        # Add the detections to the list
        for loc in locations:
            # Get the top-left and bottom-right coordinates of the bounding box
            x = loc[0]
            y = loc[1]
            w = template.shape[1]
            h = template.shape[0]

            # Add the detection to the list with its bounding box
            detections.append((x, y, x + w, y + h))

    # Apply non-maximum suppression to keep only the most confident and non-overlapping detections
    detections = non_max_suppression(np.array(detections), probs=None, overlapThresh=0.1)

    # Draw bounding boxes around the detected balls
    for (x1, y1, x2, y2) in detections:
        cv2.rectangle(image, (x1, y1), (x2, y2), (0, 255, 0), 2)

    return image

def main():
    camera_index = 1

    # Check if the ROI file exists
    if os.path.exists(ROI_FILE):
        roi_points = np.load(ROI_FILE)
    else:
        # Initialize the video capture object
        cap = cv2.VideoCapture(camera_index)

        while True:
            # Read the frame from the camera
            ret, frame = cap.read()

            if not ret:
                print("Failed to read frame from camera.")
                break

            # Flip the frame horizontally for a mirror effect
            frame = cv2.flip(frame, 1)

            # Select the ROI
            roi_points = select_roi(frame)

            # Save the ROI points to a file
            np.save(ROI_FILE, roi_points)

            # Release the video capture object and close the windows
            cap.release()
            cv2.destroyAllWindows()

            break

    # Initialize the video capture object
    cap = cv2.VideoCapture(camera_index)

    while True:
        # Read the frame from the camera
        ret, frame = cap.read()

        if not ret:
            print("Failed to read frame from camera.")
            break

        # Flip the frame horizontally for a mirror effect
        frame = cv2.flip(frame, 1)

        # Transform the image using the ROI
        transformed_image = transform_image(frame, roi_points)

        # Track the balls in the transformed image
        output_frame = track_balls(transformed_image)

        # Display the output frame
        cv2.imshow("Ball Tracking", output_frame)

        # Check if the user has pressed the 'q' key
        if cv2.waitKey(1) & 0xFF == ord("q"):
            break

    # Release the video capture object and close the windows
    cap.release()
    cv2.destroyAllWindows()

if __name__ == "__main__":
    main()
