import { createRouter, createWebHistory } from "vue-router";
import { useUserStore } from "@/store";

const userGuard = (to, from, next) => {
  const userStore = useUserStore();

  if (userStore.isLoggedIn) {
    next();
  } else {
    next("/");
  }
};

const adminGuard = (to, from, next) => {
  const userStore = useUserStore();

  if (userStore.isLoggedIn) {
    next();
  } else {
    next("/");
  }
};

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: () => import("../views/HomeView.vue"),
    },
    {
      path: "/search",
      name: "search",
      component: () => import("../views/SearchView.vue"),
    },
    {
      path: "/contact",
      name: "contact",
      component: () => import("../views/ContactUsView.vue"),
    },
    {
      path: "/auth",
      name: "auth",
      component: () => import("../views/LoginView.vue"),
    },
    {
      path: "/user",
      name: "user",
      component: () => import("../views/UserProfileView.vue"),
      beforeEnter: userGuard,
      meta: { requiresAuth: true },
    },
    {
      path: "/admin",
      name: "admin",
      component: () => import("../views/AdminView.vue"),
      beforeEnter: adminGuard,
      meta: { requiresAuth: true },
    },
    {
      path: "/:category",
      name: "category",
      component: () => import("../views/CategoryView.vue"),
    },
    {
      path: "/:category/:subcategory",
      name: "subcategory",
      component: () => import("../views/SubcategoryView.vue"),
    },
    {
      path: "/:category/:subcategory/:productSlug",
      name: "product",
      component: () => import("../views/ProductDetailsView.vue"),
    },
    {
      path: "/:pathMatch(.*)*",
      name: "not-found",
      component: () => import("../views/NotFoundView.vue"),
    },
  ],
});

export default router;
