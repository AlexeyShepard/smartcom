///Стартовая точка программы

import Vue from 'vue';
import App from './App.vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue';
import axios from 'axios';
import router from './router';
import store from './store';


Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(axios);


import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

Vue.config.productionTip = false;
Vue.prototype.$ApiUrl = 'http://localhost:11549';

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
