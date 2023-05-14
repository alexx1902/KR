const routes=[
    {path:'/home',component:home},
    {path:'/employee',component:employee},
    {path:'/department',component:department}
]
//Предыдущая версия роутера!!!!!!!
/*const router=new VueRouter({
    routes

    })
    const app = new Vue({
        router
    }).$mount('#app')*/
    const router = VueRouter.createRouter({
        history: VueRouter.createWebHashHistory(),
        routes
      })
      const app = Vue.createApp({})
      app.use(router)
      app.mount('#app')