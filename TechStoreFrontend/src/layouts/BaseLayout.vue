<script setup>
import TheTopBar from "@/components/layout/TheTopBar.vue";
import TheHeader from "@/components/layout/TheHeader.vue";
import TheNewsletters from "@/components/layout/TheNewsletters.vue";
import TheFooter from "@/components/layout/TheFooter.vue";
</script>

<template>
  <div class="baselayout">
    <the-top-bar />
    <the-header class="baselayout__header" />
    <main class="baselayout__main">
      <slot />
    </main>
    <the-newsletters class="baselayout__newsletters" />
    <the-footer class="baselayout__footer" />
  </div>
</template>

<style scoped>
.baselayout__main {
  min-height: calc(100vh - 128px - 498px);
}
</style>
