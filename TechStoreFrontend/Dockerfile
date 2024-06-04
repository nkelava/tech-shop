FROM node:lts-alpine

# make the 'app' folder the current working directory
WORKDIR /app

RUN npm install -g pnpm

# copy both 'package.json' and 'package-lock.json' (if available)
COPY package*.json pnpm-lock.yaml ./

# install project dependencies
RUN pnpm install

# copy project files and folders to the current working directory (i.e. 'app' folder)
COPY . .

EXPOSE 5173

CMD [ "pnpm", "run", "dev" ]