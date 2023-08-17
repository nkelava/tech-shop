import { createRouter, createWebHistory } from "vue-router";
import { getCategoryBySlug } from "@/database/services/categoryService.js";

function checkIfCategoryExists(category) {
  return getCategoryBySlug(category) ? true : false;
}

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
      name: "/user",
      component: () => import("../views/UserProfileView.vue"),
    },
    {
      path: "/:category",
      name: "category",
      component: () => import("../views/CategoryView.vue"),
      beforeEnter: (to, from, next) => {
        const categoryExists = checkIfCategoryExists(to.params.category);
        categoryExists ? next() : next({ name: "not-found" });
      },
    },
    {
      path: "/:category/:subcategory",
      name: "subcategory",
      component: () => import("../views/SubcategoryView.vue"),
    },
    {
      path: "/:category/:subcategory/:productId",
      name: "product",
      component: () => import("../views/ProductDetailsView.vue"),
    },
    {
      path: "/admin",
      name: "admin",
      component: () => import("../views/AdminView.vue"),
    },
    {
      path: "/:pathMatch(.*)*",
      name: "not-found",
      component: () => import("../views/NotFoundView.vue"),
    },
  ],
});

export default router;
