<script setup>
import { RouterLink } from "vue-router";
import DefaultImage from "@/assets/images/test/products/defaultProductImage.png";

const props = defineProps(["category", "subcategory"]);
console.log("subcategory: ", props.subcategory);
</script>

<template>
  <!-- TODO: add favorites and cart icon on hover -->
  <router-link
    :to="{
      name: 'subcategory',
      params: {
        category: props.category.slug,
        subcategory: props.subcategory.slug,
      },
    }"
  >
    <div class="card">
      <img
        :src="props.subcategory.imageURL"
        @error="$event.target.src = DefaultImage"
        class="card__image"
        alt="subcategory"
      />
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
  text-align: center;
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  box-shadow: 7px 5px 5px var(--ts-c-primary-darker);
  color: var(--ts-c-text-dark);
}

.card__image {
  width: 100%;
  height: 100%;
  max-height: 200px;
  border-radius: 10px 10px 0 0;
  object-fit: cover;
}

.card__title {
  margin: 0.5rem;
  font-size: var(--ts-text-size-lg);
}

.card__title a {
  color: var(--ts-c-text-dark);
}
</style>
