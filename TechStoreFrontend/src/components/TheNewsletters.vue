<template>
  <div class="newsletters">
    <div class="left">
      <div>
        <h1>Subscribe to newsletters</h1>
        <h3>Lorem ipsum dolor sit.</h3>
      </div>
    </div>
    <div class="right">
      <input type="text" placeholder="Enter yout email..." />
      <button>Subscribe</button>
    </div>
  </div>
</template>

<style scoped>
.newsletters {
  background-color: var(--ts-c-bg-highlight);
  height: 200px;
  margin-bottom: 3rem;
  display: flex;
}

.left {
  color: var(--ts-c-text-dark);
}

.left,
.right {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
}
</style>
