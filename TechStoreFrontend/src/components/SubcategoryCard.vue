<script setup>
import { RouterLink } from "vue-router";

const props = defineProps(["category", "subcategory"]);
</script>

<template>
  <!-- TODO: add favorites and cart icon on hover -->
  <router-link
    :to="{
      name: 'subcategory',
      params: {
        category: props.category,
        subcategory: props.subcategory.slug,
      },
    }"
  >
    <div class="card">
      <img :src="props.subcategory.img" alt="" class="card__image" />
      <h2 class="card__title">
        {{ props.subcategory.name }}
      </h2>
    </div>
  </router-link>
</template>

<style scoped>
.card {
  width: 100%;
  max-width: 300px;
  height: 100%;
  max-height: 300px;
  padding: 1rem;
  text-align: center;
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  box-shadow: 7px 5px 5px var(--ts-c-primary-darker);
  color: var(--ts-c-text-dark);
}

.card__image {
  width: 100%;
  max-width: 250px;
  height: 100%;
  max-height: 200px;
}

.card__title {
  margin: 0.5rem;
  font-size: var(--ts-text-size-lg);
}

.card__title a {
  color: var(--ts-c-text-dark);
}
</style>
