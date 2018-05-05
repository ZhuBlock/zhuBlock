# Requirements

Minimal requirements to get you up & going:
- git
- npm (recommended v.6.0.0+)
- yarn (recommended v1.6.0+)
- docker v17.09.1-ce+
- docker-compose v1.12.0+

# Installation

1. Clone this repository with:
```bash
git clone https://github.com/ZhuBlock/zhuBlock.git
```

2. Start installation process with:
```bash
cd zhuBlock
make install
```

3. After everything is installed, just start everything up with:
```bash
make start
```
This command should start all the docker-containers for neo local blockchain, start the Zhu-Block container and launch the nOS client.

IMPORTANT - this should be done only once (the first time you run "make start" command):

