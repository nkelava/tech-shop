import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: () => import("../views/HomeView.vue"),
    },
    {
      path: "/contact",
      name: "contact",
      component: () => import("../views/ContactUsView.vue"),
    },
    {
      path: "/login",
      name: "login",
      component: () => import("../views/LoginView.vue"),
    },
    {
      path: "/:category",
      name: "category",
      component: () => import("../views/CategoryView.vue"),
    },
  ],
});

export default router;
