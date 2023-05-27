<script setup>
import { RouterView } from "vue-router";
import TheHeader from "@/components/TheHeader.vue";
import TheNewsletters from "@/components/TheNewsletters.vue";
import TheFooter from "@/components/TheFooter.vue";
</script>

<template>
  <div>
    <TheHeader />
    <main>
      <RouterView />
    </main>
    <TheNewsletters />
    <TheFooter />
  </div>
</template>

<style scoped>
main {
  min-height: calc(100vh - 128px - 498px);
}
</style>
