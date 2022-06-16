// const http = require('http');
// const fs = require('fs');

// const port = 3000;

// http.createServer((req, res) => {
//   console.log(`Запрошенный адрес: ${req.url}`);
//   // получаем путь после слеша
//   const filePath = req.url.substring(1);
//   // смотрим, есть ли такой файл
//   fs.access(filePath, fs.constants.R_OK, (err) => {
//     // если произошла ошибка - отправляем статусный код 404
//     if (err) {
//       res.statusCode = 404;
//       res.end('Resourse not found!');
//     } else {
//       fs.createReadStream(filePath).pipe(res);
//     }
//   });
// }).listen(port, () => {
//   console.log('Server started at 3000');
// });
// const http = require("http");
// const fs = require("fs");
   
// http.createServer(function(request, response){
       
//     fs.readFile("index.html", function(error, data){   
//         response.end(data);
//     });
     
// }).listen(3000, function(){
//     console.log("Server started at 3000");
// });
const http = require("http");
const fs = require("fs");
   
http.createServer(function(request, response){
       
    let filePath = "index.html";
    if(request.url !== "/"){
        // получаем путь после слеша
        filePath = request.url.substring(1);
    }
    fs.readFile(filePath, function(error, data){
               
        if(error){
                   
            response.statusCode = 404;
            response.end("Resourse not found!");
        }   
        else{
            response.end(data);
        }
    });
     
}).listen(3000, function(){
    console.log("Server started at 3000");
});