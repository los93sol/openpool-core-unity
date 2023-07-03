import cv2
import depthai
from depthai_sdk import OakCamera

# Define variables for ROI selection
selected_roi = None
roi_selected = False

def select_roi(event, x, y, flags, param):
    global selected_roi, roi_selected

    if event == cv2.EVENT_LBUTTONDOWN:
        selected_roi = (x, y)
    elif event == cv2.EVENT_LBUTTONUP:
        roi_selected = True
        selected_roi += (x, y)

# Download & deploy a model from Roboflow universe:
# # https://universe.roboflow.com/david-lee-d0rhs/american-sign-language-letters/dataset/6
key = '5ZbJYtUJv7fCVCUKfpTB' # Fake API Key, replace with your own
fps = 10

display_width = 2560
display_height = 1080

with OakCamera() as oak:
    color = oak.create_camera('color')

    camera_width = color.node.getResolutionWidth() # 1920
    camera_height = color.node.getResolutionHeight() # 1080

    offset_width = display_width - camera_width
    offset_height = display_height - camera_height

    color.node.setFps(fps)
    color.node.setImageOrientation(depthai.CameraImageOrientation.ROTATE_180_DEG)
    
    # Create an OpenCV window to display the image
    cv2.namedWindow("ROI Selection")
    cv2.setMouseCallback("ROI Selection", select_roi)

    # Capture the first frame from the camera
    frame = color.get_frame()

    while not roi_selected:
        # Display the frame in the OpenCV window
        cv2.imshow("ROI Selection", frame)

        # Wait for a key press and check if the 'q' key is pressed to exit
        key = cv2.waitKey(1) & 0xFF
        if key == ord("q"):
            break

    # Close the OpenCV window
    cv2.destroyAllWindows()

    # Check if ROI was selected
    if roi_selected:
        # Crop the image based on the selected ROI
        x1, y1, x2, y2 = selected_roi
        cropped_frame = frame[y1:y2, x1:x2]

        model_config = {
            'source':'roboflow', # Specify that we are downloading the model from Roboflow
            'model':'billiard-balls-orjvo/1',
            'key':key
        }

        nn = oak.create_nn(model_config, color)

        def process_results(results):
            # Extract the detected object coordinates
            for detection in results.detections:
                if detection.label == 'son':
                    x, y = detection.centroid()
                    print(f"{detection.label}: ({x}, {y}), ({(x + offset_width)/display_width}, {(y + offset_height)/display_height})")

        # Perform further processing or pass the cropped frame to the neural network model
        # ...

        oak.visualize(nn, fps=True, callback=process_results)
        oak.start(blocking=True)
    else:
        print("ROI selection cancelled")
