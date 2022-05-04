import { render } from "react-dom";
import React from "react";
import HelloWorld from "./components/helloworld";

const root = document.getElementById("app");

render(
    <HelloWorld/>, root
);