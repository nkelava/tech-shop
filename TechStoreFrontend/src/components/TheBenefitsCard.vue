<script setup>
const props = defineProps(["icon", "title", "content"]);
</script>

<template>
  <div class="benefit-card">
    <img :src="props.icon" alt="home icon" class="benefit-card__img" />
    <div class="benefit-card__content">
      <h3>{{ props.title }}</h3>
      <p>{{ props.content }}</p>
    </div>
  </div>
</template>

<style scoped>
.benefit-card {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 0.5rem 1rem;
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  color: var(--ts-c-text-dark);
  min-height: 120px;
  max-width: 500px;
}

.benefit-card__img {
  width: 100%;
  max-width: 40px;
  height: 100%;
  max-height: 40px;
  margin-right: 0;
}

.benefit-card__content {
  text-align: center;
}

.benefit-card__content h3 {
  margin-top: 0.5rem;
}

.benefit-card__content p {
  color: var(--ts-c-primary-soft);
}

@media only screen and (min-width: 48em) {
  .benefit-card {
    flex-direction: row;
    padding-inline: 1.5rem;
  }
  .benefit-card__img {
    max-width: 48px;
    max-height: 48px;
    margin-right: 1rem;
  }
  .benefit-card__content {
    text-align: start;
  }

  .benefit-card__content h3 {
    margin-top: 0;
    font-size: 1.125rem;
  }
}

@media only screen and (min-width: 100em) {
  .benefit-card {
    justify-content: left;
    padding-inline: 1.5rem;
    border-radius: 0;
    border-right: 1px solid var(--ts-c-bg-dark);
  }

  .benefit-card:first-child {
    border-radius: 10px 0 0 10px;
  }

  .benefit-card:last-child {
    border-radius: 0 10px 10px 0;
  }
}
</style>
