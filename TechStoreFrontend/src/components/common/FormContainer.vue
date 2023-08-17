<script setup>
const props = defineProps(["title"]);
</script>

<template>
  <div class="form-container">
    <h2>{{ props.title }}</h2>
    <slot></slot>
  </div>
</template>

<style scoped>
.form-container {
  max-width: 700px;
  margin: 0 auto 100px;
  text-align: center;
}
</style>
