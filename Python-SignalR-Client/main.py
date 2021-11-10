#!/usr/bin/python
from Client.Client import Test
from DownLoad.DownLoad import HttpTools
import time
import asyncio
async def tet():
    await HttpTools().downLoadFile('Output.tar')


if __name__ == '__main__':

    # test = Test('http://localhost:7777/chatHub')
    test = Test('http://192.168.247.131:32112/chatHub')
    test.connect()
    # test.sendmessage('recive', ['5566'])

    message = None

    while True:
        time.sleep(2)
        test.sendmessage('recive', ['5566'])
