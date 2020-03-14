import Vue from "vue";
import axios from "axios";
const template = require("./index.html");

Vue.component("ajax-form", {
    template: template,
    props: {
        requestFormUrl: String,
        requestFormData: null,
        requestFormSelector: String
    },
    data() {
        return {
            requestForm: {
                content: "",
                url: this.requestUrl,
                data: this.requestData,
                formSelector: this.requestFormSelector,
                context: null
            }
        };
    },
    watch: {
        requestFormUrl(newValue) {
            this.requestForm.url = newValue;
            this.getForm(newValue, this.requestForm.data);
        },
        requestFormData(newValue) {
            const context = this;
            this.requestForm.data = newValue;
            this.getForm(this.requestForm.url, newValue)
                .then(e => { context.updateFormContent(e.data); });
        }
    },
    methods: {
        getForm(url, data) {
            return axios.get(url, { data: data });
        },
        updateFormContent(e) {
            console.log(e);
            this.requestForm.content = e.data;
            window.setTimeout(captureForm, 1000);
        },
        captureForm() {
            if (!requestForm.formSelector)
                requestForm.formSelector = "form";

            this.requestForm.context = this.$el.querySelector(this.requestForm.formSelector);
        }
    },
    mounted() {
        this.getForm(this.requestForm.url, this.requestForm.data);
    }});