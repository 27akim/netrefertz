# Стадия сборки
FROM node:latest as build

WORKDIR /app

COPY package*.json ./

RUN npm ci

RUN npm install -g @angular/cli

COPY . .

# Выводим текущий статус директории перед сборкой
RUN ls -la /app

RUN npm run build

# Добавляем проверку содержимого директории сборки
RUN ls -la /app/dist

# Стадия nginx
FROM nginx:latest

COPY ./nginx.conf /etc/nginx/conf.d/default.conf
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /app/dist/book-management/browser /usr/share/nginx/html

EXPOSE 80
