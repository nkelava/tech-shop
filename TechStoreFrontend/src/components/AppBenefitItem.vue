<script setup>
const { icon, title, text } = defineProps(["icon", "title", "text"]);
</script>

<template>
  <div class="benefit-item">
    <img :src="icon" alt="home icon" class="benefit-item__img" />
    <div class="benefit-item__content">
      <h2>{{ title }}</h2>
      <p>{{ text }}</p>
    </div>
  </div>
</template>

<style scoped>
.benefit-item {
  background-color: var(--ts-c-bg-light);
  border-right: 1px solid var(--ts-c-bg-dark);
  color: var(--ts-c-text-dark);
  padding: 1rem 3rem;
  display: flex;
  justify-content: center;
  align-items: center;
}

.benefit-item > * {
  flex-wrap: wrap;
}

.benefit-item:first-child {
  border-radius: 10px 0 0 10px;
}

.benefit-item:last-child {
  border-radius: 0 10px 10px 0;
}

.benefit-item__img {
  width: 48px;
  height: 48px;
  margin-right: 1rem;
}

.benefit-item__content h2 {
  font-size: 1rem;
}

.benefit-item__content p {
  color: var(--ts-c-primary-soft);
}

@media only screen and (max-width: 1200px) {
  .benefit-item,
  .benefit-item:first-child,
  .benefit-item:last-child {
    border-radius: 10px;
  }
}

@media only screen and (max-width: 600px) {
  .benefit-item {
    border-radius: 10px;
  }
}
</style>
