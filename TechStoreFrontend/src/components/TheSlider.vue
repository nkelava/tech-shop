<script setup>
const { height } = defineProps(["height"]);
</script>

<template>
  <div class="slider">
    <img src="" alt="" />
  </div>
</template>

<style scoped>
.slider {
  background: var(--ts-c-bg-light);
  height: v-bind(height);
}
</style>
