<template>
  <div class="container">
    <h1>404 - Page Not Found</h1>
    <h3 class="subtitle">
      The page you are looking for might have been removed or temprarily unavailable.
    </h3>
    <v-btn class="back-btn" to="/" flat>Go back to home</v-btn>
  </div>
</template>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding-top: 5rem;
}

.subtitle {
  color: var(--ts-c-secondary-dark);
}

.back-btn {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  font-weight: bold;
  margin-top: 3rem;
  text-decoration: none;
}
</style>
