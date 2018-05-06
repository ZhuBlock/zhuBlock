DEFAULT: install 

# Colors
NO_COLOR 	 = \033[0m
BLACK 		 = \033[0;30m
DARK_GREY	 = \033[1;30m
RED   		 = \033[0;31m
LIGHT_RED 	 = \033[1;31m
GREEN 		 = \033[0;32m
LIGHT_GREEN  = \033[1;32m
BROWN_ORANGE = \033[0;33m
YELLOW 		 = \033[1;33m
BLUE 		 = \033[0;34m
LIGHT_BLUE 	 = \033[1;34m
PURPLE 		 = \033[0;35m
LIGHT_PURPLE = \033[1;35m
CYAN 		 = \033[0;36m
LIGHT_CYAN 	 = \033[1;36m
LIGHT_GREY   = \033[0;37m
WHITE 		 = \033[1;37m

ECHO_PREFIX = ${GREEN}[Zhu-Block]${NO_COLOR}

install: clean nos-deps zhu-block

zhu-block:
	@echo "\n"
	@echo "${ECHO_PREFIX} Building Zhu-Block container image..."
	@cd ./docker && docker-compose build --force-rm --no-cache --build-arg user=$$(id -u):$$(id -g) zhu-block > /dev/null

	@echo "\n"
	@echo "${ECHO_PREFIX} Installing Zhu-Block dependencies..."
	@cd ./docker && ./scripts/docker-run.sh yarn install

	@echo "\n"
	@echo "${ECHO_PREFIX} Installation completed!"
	@echo "${YELLOW}IMPORTANT!${NO_COLOR} Make sure you have added 127.0.0.1 neo-nodes to your host file if you want to use testInvoke from your nOS dApp's."

start:
	@echo "${ECHO_PREFIX} Starting NEO local blockchain..."
	@cd ./nOS/nos-local/neo-local && docker-compose up -d

	@echo "\n"
	@echo "${ECHO_PREFIX} Starting Zhu-Block container..."
	@cd ./docker && docker-compose up -d

	@echo "\n"
	@echo "${ECHO_PREFIX} Starting nOS client..."
	@cd ./nOS/client && yarn start

stop:
	@echo "${ECHO_PREFIX} Stopping NEO local blockchain..."
	@cd ./nOS/nos-local/neo-local && docker-compose stop

	@echo "\n"
	@echo "${ECHO_PREFIX} Removing Zhu-Block container..."
	@cd ./docker && docker-compose down

clean:
	@echo "${ECHO_PREFIX} Cleaning Zhu-Block project..."
	@rm -rf ./node_modules ./nOS/client ./nOS/nos-local

nos-deps:
	@echo "\n"
	@echo "${ECHO_PREFIX} Cloning nOS client..."
	@git clone https://github.com/nos/client.git ./nOS/client

	@echo "\n"
	@echo "${ECHO_PREFIX} Installing nOS client dependencies..."
	@cd ./nOS/client && yarn install

	@echo "\n"
	@echo "${ECHO_PREFIX} Cloning NEO local blockchain..."
	@git clone https://github.com/nos/nos-local.git ./nOS/nos-local

	@echo "\n"
	@echo "${ECHO_PREFIX} Installing NEO local blockchain..."
	@cd ./nOS/nos-local && git submodule update --init && cp ./contracts/*.py ./neo-local/smart-contracts
	@cd ./nOS/nos-local/neo-local && docker-compose build --force-rm --no-cache > /dev/null

remove-all: stop
	@echo "\n"
	@echo "${ECHO_PREFIX} Removing NEO local blockchain..."
	@cd ./nOS/nos-local/neo-local && docker-compose down

init-neo-local:
	@echo "${ECHO_PREFIX} Starting NEO local prompt..."
	@docker exec -it neo-python np-prompt -v -p
