<template>
  <div class="grid">
    <slot />
  </div>
</template>

<style scoped>
.grid {
  padding: 2rem 0;
  display: grid;
  grid-template-columns: repeat(auto-fit, 250px);
  justify-content: center;
  gap: 1rem;
}
</style>
