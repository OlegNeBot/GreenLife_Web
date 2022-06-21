const { src, dest, parallel, series } = require('gulp');
const exec = require('exec-chainable');
const webpack = require('webpack-stream');
let { run } = require('gulp-dotnet-cli');

function startserver() {
  exec('nodemon server.js').then(function (stdout) {
    console.log(stdout);
  });
}

function startwebpack() {
  return src('./src/index.tsx')
  .pipe(webpack(require('./webpack.config.js')))
  .pipe(dest('build/dist'));
}

function starttasks() {
  console.log('Dev tasks started!');
}

function startbackserver() {
  return src('../backend/GreenLife/Server/Server.csproj', {read:false})
  .pipe(run());
}

exports.startserver = startserver;

exports.startwebpack = startwebpack;

exports.default = parallel(startwebpack, startserver, 
  startbackserver, 
  starttasks);