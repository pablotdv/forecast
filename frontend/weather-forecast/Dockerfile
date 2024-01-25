FROM node

WORKDIR /app

COPY package.json yarn.lock ./
COPY .env.docker .env

RUN yarn install

COPY . .

RUN yarn build

EXPOSE 3000

CMD [ "yarn", "start" ]