from signalrcore.hub_connection_builder import HubConnectionBuilder
import logging
from DownLoad.DownLoad import HttpTools
import asyncio
class Test:
    def __init__(self, url):
        self.url = url
        self.hub_connection = HubConnectionBuilder()\
        .with_url(url)\
        .configure_logging(logging.DEBUG)\
        .with_automatic_reconnect({
            "type": "raw",
            "keep_alive_interval": 10,
            "reconnect_interval": 5,
            "max_attempts": 5
        }).build()


    def connect(self):
        self.registerMethod()
        self.hub_connection.start()
        

    def ReceiveMessage(self, message):
        print(message)



    def sendmessage(self, methodname, message):
        self.hub_connection.send(methodname, message)


    def Download(self,fileName):
        asyncio.run(HttpTools().downLoadFile(fileName[0]))
        
        
    def registerMethod(self):
        self.hub_connection.on("ReceiveMessage", self.ReceiveMessage)
        self.hub_connection.on("download", self.Download)
        self.hub_connection.on_error(lambda data: print(f"An exception was thrown closed{data.error}"))
        self.hub_connection.on_open(lambda: print("connection opened and handshake received ready to send messages"))
        self.hub_connection.on_close(lambda: print("connection closed"))

