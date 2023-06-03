<script setup>
import { RouterLink } from "vue-router";

const { category, subcategory } = defineProps(["category", "subcategory"]);
</script>

<template>
  <!-- TODO: add favorites and cart icon on hover -->
  <div class="card">
    <img :src="subcategory.img" alt="" class="card__image" />
    <h2 class="card__title">
      <router-link
        :to="{
          name: 'subcategory',
          params: {
            category,
            subcategory: subcategory.slug,
          },
        }"
      >
        {{ subcategory.name }}
      </router-link>
    </h2>
  </div>
</template>

<style scoped>
.card {
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  box-shadow: 7px 5px 5px var(--ts-c-primary-darker);
  color: var(--ts-c-text-dark);
  padding: 1rem;
  text-align: center;
  width: 200px;
}

.card__image {
  width: 150px;
  height: 150px;
}

.card__title {
  font-size: 1rem;
  margin: 0.5rem;
}
</style>
