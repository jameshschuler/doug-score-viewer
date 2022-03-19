<template>
  <article class="message is-info">
    <div class="message-header" v-if="title">
      <p>{{ props.title }}</p>
      <button class="delete"></button>
    </div>
    <div class="message-body" v-html="props.message"></div>
  </article>
</template>
<script lang="ts" setup>
const props = defineProps({
  title: {
    type: String,
  },
  message: {
    type: String,
    required: true,
  },
});
</script>
<style lang="scss"></style>
