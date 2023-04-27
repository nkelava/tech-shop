<script setup>
import pcImage from "@/assets/images/test/products/pc.png";
</script>

<template>
  <div class="card">
    <img :src="pcImage" alt="" :width="200" :height="200" />
    <h2>Lorem ipsum</h2>
    <p class="description">Lorem ipsum dolor, sit amet consectetur adipisicing elit.</p>
    <h3 class="price">123,49$</h3>
    <button>View more</button>
  </div>
</template>

<style scoped>
.card {
  background-color: var(--my-s);
  color: var(--my-p);
  padding: 1rem;
  margin-top: 40px;
  display: block;
  text-align: center;
  width: 250px;
}

.price {
  margin: 15px 0;
}

.description {
  color: #847c5f;
}

button {
  padding: 10px;
  background-color: var(--my-p);
  color: var(--my-s);
}
</style>
