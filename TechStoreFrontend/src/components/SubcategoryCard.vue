<script setup>
import { RouterLink } from "vue-router";

const props = defineProps(["category", "subcategory"]);
</script>

<template>
  <!-- TODO: add favorites and cart icon on hover -->
  <div class="card">
    <img :src="props.subcategory.img" alt="" class="card__image" />
    <h2 class="card__title">
      <router-link
        :to="{
          name: 'subcategory',
          params: {
            category: props.category,
            subcategory: props.subcategory.slug,
          },
        }"
      >
        {{ props.subcategory.name }}
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
  width: 100%;
  max-width: 300px;
  height: 100%;
  max-height: 300px;
}

.card__image {
  width: 100%;
  max-width: 250px;
  height: 100%;
  max-height: 200px;
}

.card__title {
  font-size: 1rem;
  margin: 0.5rem;
}
</style>
