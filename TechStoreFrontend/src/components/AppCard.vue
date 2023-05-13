<script setup>
import pcImage from "@/assets/images/test/products/pc.png";
</script>

<template>
  <div class="card">
    <img :src="pcImage" alt="" :width="200" :height="200" />
    <h2>Lorem ipsum</h2>
    <p class="card-summary">Lorem ipsum dolor, sit amet consectetur adipisicing elit.</p>
    <p class="card-price">123,49$</p>
    <button>View more</button>
  </div>
</template>

<style scoped>
.card {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  margin-top: 40px;
  padding: 1rem;
  display: block;
  text-align: center;
  width: 250px;
}

.card-pri {
  margin: 15px 0;
}

.card-summary {
  color: var(--ts-c-secondary-darker);
}

button {
  background-color: var(--ts-c-bg-dark);
  color: var(--ts-c-text-light);
  padding: 10px;
}
</style>
