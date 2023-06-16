<script setup>
const props = defineProps(["imgUrl", "imgAlt"]);
</script>

<template>
  <v-card class="d-flex align-center" variant="text">
    <v-img :src="props.imgUrl" :alt="props.imgAlt" class="mr-2" />
    <v-card-item class="pa-0"><slot /></v-card-item>
  </v-card>
</template>
