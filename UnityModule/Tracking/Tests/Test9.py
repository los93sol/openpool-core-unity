import cv2
from pythonosc import udp_client

class OpenPool:
    def __init__(self, ipAddress):
        self.client = udp_client.SimpleUDPClient(ipAddress, 7000)
        self.frame = 0
        self.table_width = 1.0  # Adjust as per your table dimensions
        self.table_height = 1.0  # Adjust as per your table dimensions
        print("Address: ", ipAddress)

    def sendOsc(self, url, params):
        self.client.send_message(url, params)
        print("Sent: ", url, " : ", params)

    def sendBall(self, x, y):
        if self.frame >= 100:
            self.frame = 0
        else:
            self.frame += 1
        
        # Scale the x and y values
        scaled_x = x / self.table_width
        scaled_y = y / self.table_height
        
        self.sendOsc("/ball", [self.frame, scaled_x, scaled_y])

def detect_billiard_balls():
    # Open the video capture
    cap = cv2.VideoCapture(0)

    # Increase the frame rate
    cap.set(cv2.CAP_PROP_FPS, 60)

    # Create an instance of the OpenPool class
    op = OpenPool("127.0.0.1")  # Replace with the actual IP address

    while True:
        # Read the current frame
        ret, frame = cap.read()

        # Mirror the frame horizontally
        frame = cv2.flip(frame, 1)

        # Mirror the frame vertically
        frame = cv2.flip(frame, 0)

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

                # Filter out small detections
                if 4 < radius < 12:
                    # Draw a circle around the detected ball
                    cv2.circle(frame, center, radius, (0, 255, 0), 2)

                    # Send OSC message with ball coordinates
                    op.sendBall(x, y)

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
