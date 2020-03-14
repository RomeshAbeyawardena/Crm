require("./scss");
import Vue from "vue";
import vueSetup from "./vue-setup";

window.addEventListener("load", () => new Vue(vueSetup));