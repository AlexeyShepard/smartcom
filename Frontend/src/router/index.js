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
    path: '/auth',
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
  /*{
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about"  '../views/About.vue')
  }*/
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router