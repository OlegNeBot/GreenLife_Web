const http = require('http');
const fs = require('fs');

const port = 3000;

http.createServer((req, res) => {
  console.log(`Запрошенный адрес: ${req.url}`);
  // получаем путь после слеша
  const filePath = req.url.substring(1);
  // смотрим, есть ли такой файл
  req.headers = {
    'Access-Control-Allow-Origin': '*',
  };
  fs.access(filePath, fs.constants.R_OK, (err) => {
    // если произошла ошибка - отправляем статусный код 404
    if (err) {
      res.statusCode = 404;
      res.end('Resourse not found!');
    } else {
      fs.createReadStream(filePath).pipe(res);
    }
  });
}).listen(port, () => {
  console.log('Server started at 3000');
});
