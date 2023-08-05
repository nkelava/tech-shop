<script setup>
const props = defineProps(["imgUrl", "imgAlt"]);
</script>

<template>
  <v-card class="contact-info" variant="text">
    <img :src="props.imgUrl" :alt="props.imgAlt" class="mr-2" />
    <v-card-item class="contact-info__item"><slot /></v-card-item>
  </v-card>
</template>

<style scoped>
.contact-info {
  display: flex;
  align-items: center;
  margin-right: 1rem;
}

.contact-info__item {
  padding: 0;
}
</style>
