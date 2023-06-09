<script setup>
import { RouterLink } from "vue-router";
</script>

<template>
  <div class="header__nav">
    <router-link class="nav__item" to="/">Home</router-link>
    <router-link class="nav__item" to="/laptops">Laptops</router-link>
    <router-link class="nav__item" to="/computers">Computers</router-link>
    <router-link class="nav__item" to="/components">Components</router-link>
    <router-link class="nav__item" to="/peripherals">Peripherals</router-link>
    <router-link class="nav__item" to="/software">Software</router-link>
    <router-link class="nav__item" to="/contact">Contact Us</router-link>
  </div>
</template>

<style scoped>
.header__nav {
  border-bottom: 1px solid var(--ts-c-primary-darker);
  justify-content: center;
  gap: 1rem;
  padding: 10px 0;
}

.nav__item {
  color: var(--ts-c-text-light);
}
</style>
