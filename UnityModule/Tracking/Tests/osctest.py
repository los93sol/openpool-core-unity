#! /usr/bin/env python
# -*- coding: utf-8 -*-

'''
OscTest.py
'''

from pythonosc import udp_client

class OpenPool:
    def __init__(self, ipAddress):
        self.client = udp_client.SimpleUDPClient(ipAddress, 7000)
        self.frame = 0
        print("address : ", ipAddress)

    def sendOsc(self, url, params):
        self.client.send_message(url, params)
        print("send : ", url, " : ", params)

    def sendBall(self, x, y):
        if self.frame >= 100:
            self.frame = 0
        else:
            self.frame += 1
        self.sendOsc("/ball", [self.frame, x, y])

    def sendPocket(self, id):
        self.sendOsc("/pocket", [id, 1])

    def sendCollision(self):
        self.sendOsc("/collision", [0.1])

    def sendQueue(self):
        self.sendOsc("/queue", [0])


if __name__ == '__main__':
    import sys
    import time

    argvs = sys.argv
    argc = len(argvs)

    if len(argvs) <= 1:
        address = "127.0.0.1"
    else:
        address = argvs[1]

    op = OpenPool(address)

    # ball position test
    time.sleep(1)
    op.sendBall(0.1, 0.1)
    time.sleep(0.05)
    op.sendBall(0.2, 0.2)
    time.sleep(0.05)
    op.sendBall(0.3, 0.3)
    time.sleep(0.05)
    op.sendBall(0.4, 0.4)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)

    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)
    time.sleep(0.05)
    op.sendBall(0.5, 0.5)

    # pocket in test
    time.sleep(1)
    op.sendPocket(0)
    time.sleep(1)
    op.sendPocket(1)
    time.sleep(1)
    op.sendPocket(2)
    time.sleep(1)
    op.sendPocket(3)
    time.sleep(1)
    op.sendPocket(4)
    time.sleep(1)
    op.sendPocket(5)

    # collision
    time.sleep(1)
    op.sendCollision()
    time.sleep(1)
    op.sendCollision()
    time.sleep(1)
    op.sendCollision()
    time.sleep(1)
    op.sendCollision()