# Requirements

Minimal requirements to get you up & going:
- git
- npm (recommended v.6.0.0+)
- yarn (recommended v1.6.0+)
- docker v17.09.1-ce+
- docker-compose v1.12.0+

# Installation

### 1. Clone repository
Clone this repository with:  
```bash
git clone https://github.com/ZhuBlock/zhuBlock.git
``` 

### 2. Installation
Start installation process with:  
```bash
cd zhuBlock
make install
```

### 3. Running the environment
After everything is installed, run this command:  
```bash
make start
```


IMPORTANT - this should be done only once (the first time you run "make start" command):  
```bash
docker exec neo-python np-prompt -p -v
```
Wait for all the blocks to download and execute following commands:
```bash
open wallet ./neo-privnet.wallet
build /smart-contracts/contract.py
import contract /smart-contracts/contract.avm 0710 05 True False
testinvoke e60a3fa8149a853eb4dff4f6ed93c931646a9e22 RegisterDomain ['AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y', 'zhu-block.nos', 'AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y', 'http://localhost:3001/']
testinvoke e60a3fa8149a853eb4dff4f6ed93c931646a9e22 GetDomain ['AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y', 'zhu-block.nos']
```
Visit zhu-block.nos in client!

### 4. Stopping the environment
When you're done developing, close the client and run this command:
```bash
make stop
```
