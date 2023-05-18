<script setup>
const { icon, title, text } = defineProps(["icon", "title", "text"]);
</script>

<template>
  <div class="benefit-card">
    <img :src="icon" alt="home icon" class="benefit-card__img" />
    <div class="benefit-card__content">
      <h2>{{ title }}</h2>
      <p>{{ text }}</p>
    </div>
  </div>
</template>

<style scoped>
.benefit-card {
  background-color: var(--ts-c-bg-light);
  border-right: 1px solid var(--ts-c-bg-dark);
  color: var(--ts-c-text-dark);
  padding: 1rem 3rem;
  display: flex;
  justify-content: center;
  align-items: center;
}

.benefit-card > * {
  flex-wrap: wrap;
}

.benefit-card:first-child {
  border-radius: 10px 0 0 10px;
}

.benefit-card:last-child {
  border-radius: 0 10px 10px 0;
}

.benefit-card__img {
  width: 48px;
  height: 48px;
  margin-right: 1rem;
}

.benefit-card__content h2 {
  font-size: 1rem;
}

.benefit-card__content p {
  color: var(--ts-c-primary-soft);
}

@media only screen and (max-width: 1200px) {
  .benefit-card,
  .benefit-card:first-child,
  .benefit-card:last-child {
    border-radius: 10px;
  }
}

@media only screen and (max-width: 600px) {
  .benefit-card {
    border-radius: 10px;
  }
}
</style>
