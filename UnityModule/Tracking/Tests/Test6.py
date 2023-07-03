import cv2

def detect_billiard_balls():
    # Open the video capture
    cap = cv2.VideoCapture(0)

    while True:
        # Read the current frame
        ret, frame = cap.read()

        # Convert the frame to grayscale
        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

        # Apply Gaussian blur to reduce noise
        blurred = cv2.GaussianBlur(gray, (7, 7), 0)

        # Perform Canny edge detection
        edges = cv2.Canny(blurred, 30, 90)

        # Find contours in the edge image
        contours, _ = cv2.findContours(edges, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

        # Iterate through the contours
        for contour in contours:
            # Approximate the contour to a circle
            approx = cv2.approxPolyDP(contour, 0.05 * cv2.arcLength(contour, True), True)

            # Check if the contour has a circular shape
            if len(approx) > 3:
                (x, y), radius = cv2.minEnclosingCircle(contour)
                center = (int(x), int(y))
                radius = int(radius)

                # Draw a circle around the detected ball
                cv2.circle(frame, center, radius, (0, 255, 0), 2)

        # Show the resulting frame
        cv2.imshow('Billiard Ball Detection', frame)

        # Break the loop if 'q' is pressed
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

    # Release the video capture and destroy all windows
    cap.release()
    cv2.destroyAllWindows()

# Run the billiard ball detection
detect_billiard_balls()
