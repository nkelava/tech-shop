<script setup>
const props = defineProps(["icon", "title", "content"]);
</script>

<template>
  <div class="benefit-card">
    <img :src="props.icon" alt="home icon" class="benefit-card__img" />
    <div class="benefit-card__content">
      <h2>{{ props.title }}</h2>
      <p>{{ props.content }}</p>
    </div>
  </div>
</template>

<style scoped>
.benefit-card {
  background-color: var(--ts-c-bg-light);
  border-right: 1px solid var(--ts-c-bg-dark);
  color: var(--ts-c-text-dark);
  display: flex;
  justify-content: left;
  align-items: center;
  min-height: 120px;
  max-width: 500px;
  padding: 1rem 3rem;
}

.benefit-card:first-child {
  border-radius: 10px 0 0 10px;
}

.benefit-card:last-child {
  border-radius: 0 10px 10px 0;
}

.benefit-card__img {
  width: 100%;
  max-width: 48px;
  height: 100%;
  max-height: 48px;
  margin-right: 2rem;
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

@media only screen and (max-width: 380px) {
  .benefit-card {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 0.5rem 1rem;
  }

  .benefit-card__img {
    width: 100%;
    max-width: 48px;
    height: 100%;
    max-height: 48px;
    margin-right: 0;
  }

  .benefit-card__content {
    text-align: center;
  }

  .benefit-card__content h2 {
    margin-top: 10px;
  }
}
</style>
