<template>
  <div>
    <div class="hero">
      <img src="" alt="" />
    </div>
    <div class="banner-container">
      <div class="banner-item">
        <h3>Lorem ipsum</h3>
        Lorem, ipsum.
      </div>
      <div class="banner-item">
        <h3>Lorem ipsum</h3>
        Lorem, ipsum.
      </div>
      <div class="banner-item">
        <h3>Lorem ipsum</h3>
        Lorem, ipsum.
      </div>
      <div class="banner-item">
        <h3>Lorem ipsum</h3>
        Lorem, ipsum.
      </div>
    </div>
  </div>
</template>

<style scoped>
.hero {
  background: var(--my-s);
  height: 500px;
}

.banner-container {
  width: 100%;
  margin-top: 40px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.banner-item {
  background-color: var(--my-s);
  color: var(--my-p);
  border: 1px solid var(--my-p);
  padding: 1rem 2rem;
}

.banner-item:first-child {
  border-radius: 10px 0 0 10px;
}

.banner-item:last-child {
  border-radius: 0 10px 10px 0;
}
</style>
