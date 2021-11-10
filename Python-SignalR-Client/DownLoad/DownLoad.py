import httpx
import os
class HttpTools:
    def __init__(self):
        ...

    async def downLoadFile(self,fileName):
        if not os.path.exists(f'D:\{fileName}'):
            client = httpx.AsyncClient(timeout=10000);
            async with client.stream('GET', f'http://192.168.247.131:32112/api/download/DownloadfromBytes?filename={fileName}') as response:
                with open(f'D:\{fileName}', 'wb') as f:
                    async for chunk in response.aiter_bytes():
                        f.write(chunk)
            
               
    