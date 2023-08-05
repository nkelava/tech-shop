<script setup>
import { onMounted, ref } from "vue";
import { RouterLink } from "vue-router";
import { getCategories } from "@/database/services/categoryService.js";

const categories = ref([]);

onMounted(() => {
  categories.value = getCategories();
});
</script>

<template>
  <nav class="nav-container">
    <router-link class="nav__item" to="/">Home</router-link>
    <router-link
      v-for="category in categories"
      :key="category.id"
      class="nav__item"
      :to="`/${category.slug}`"
    >
      {{ category.name }}
    </router-link>
    <router-link class="nav__item" to="/contact">Contact Us</router-link>
  </nav>
</template>

<style scoped>
.nav-container {
  border-bottom: 1px solid var(--ts-c-primary-darker);
  color: var(--ts-c-text-light);
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  font-size: 16px;
}

.nav__item {
  border: 3px solid transparent;
  color: var(--ts-c-text-light);
  text-decoration: none;
  text-transform: capitalize;
  margin-right: 1rem;
  padding: 5px;
}

.nav__item:hover {
  border-bottom: 3px solid var(--ts-c-bg-light);
}
</style>
