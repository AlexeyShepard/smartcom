///Роутер с описанием всех маршрутов

import Vue from 'vue';
import VueRouter from 'vue-router';
import Registration from '@/components/Registration';
import Authorization from '@/components/Authorization';
import CustomerArea from '@/components/Customer/CustomerArea';
import AdminArea from '@/components/Admin/AdminArea';

Vue.use(VueRouter)

const routes = [
  {
    path: '/reg',
    name: 'Registration',
    component: Registration
  },
  {
    path: '/',
    name: "Authorization",
    component: Authorization
  },
  {
    path: '/customer',
    name: 'CustomerArea',
    component: CustomerArea
  },
  {
    path: '/admin',
    name: 'AdminArea',
    component: AdminArea
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
