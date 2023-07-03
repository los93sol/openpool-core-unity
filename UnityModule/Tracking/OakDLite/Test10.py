import depthai
from depthai_sdk import OakCamera
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

# Download & deploy a model from Roboflow universe:
# # https://universe.roboflow.com/david-lee-d0rhs/american-sign-language-letters/dataset/6
key = '5ZbJYtUJv7fCVCUKfpTB' # Fake API Key, replace with your own
fps = 10

display_width = 3840
display_height = 2160

ipAddress = '127.0.0.1'

op = OpenPool(ipAddress)

with OakCamera() as oak:
    color = oak.create_camera('color')

    camera_width = color.node.getResolutionWidth() # 1920
    camera_height = color.node.getResolutionHeight() # 1080

    offset_width = display_width - camera_width
    offset_height = display_height - camera_height

    color.node.setFps(fps)
    color.node.setImageOrientation(depthai.CameraImageOrientation.ROTATE_180_DEG)
    
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

                offset_x = -200
                offset_y = -20

                if x > 330 and x <= 540:
                    offset_x = 35
                elif x > 540 and x <= 750:
                    offset_x = 240
                elif x > 750 and x <= 960:
                    offset_x = 460
                elif x > 960:
                    offset_x = 630

                if y >= 251 and y <= 427:
                    offset_y = 185
                elif y > 427:
                    offset_y = 375

                print(f"offset_x: {offset_x}, offset_y: {offset_y}")
                #if x < 470 and y >= 251 and y < 427:
                #    offset_y = 251
                #else if x < 470 and y >= 427:
                #    offset_y = 

                x_scaled = (x + offset_x) / camera_width
                y_scaled = (y + offset_y) / camera_height
                print(f"{detection.label}: ({x}, {y}), ({x_scaled}, {y_scaled})")
                op.sendBall(x_scaled, y_scaled)
    
    oak.visualize(nn, fps=True, callback=process_results)
    #oak.visualize(nn, fps=True)
    oak.start(blocking=True)
